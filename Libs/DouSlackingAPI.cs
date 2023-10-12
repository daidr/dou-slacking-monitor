using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DouSlackingMonitor.Libs
{
    public class DouSlackingAPI
    {
        RestClient client;
        bool TimelineUploading = false;
        int timeout = 2000;

        public DouSlackingAPI(string url, string secret)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = timeout
            };
            client = new RestClient(options);
            client.AddDefaultHeader("Authorization", secret);
        }

        public void UpdateOptions(string url, string secret)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = timeout
            };
            client = new RestClient(url);
            client.AddDefaultHeader("Authorization", secret);
        }

        private string getMessage(Exception e)
        {
            switch (e.Message)
            {
                case "Request failed with status code Unauthorized":
                    return "无权限";
                default:
                    Console.WriteLine(e.Message);
                    return e.Message;
            }
        }

        public string UploadProcess(string processName)
        { 
            // put /api/process
            var request = new RestRequest("api/process", Method.Put);
            var param = new { processName };
            request.AddJsonBody(param);

            try
            {
                client.Put(request);
            }
            catch (Exception e)
            {
                return getMessage(e);
            }
            return "正常";
        }

        public string UpdateIdle(bool idle)
        {
            // put /api/idle
            var request = new RestRequest("api/idle", Method.Put);
            var param = new { idle };
            request.AddJsonBody(param);
            try
            {
                client.Put(request);
            }
            catch (Exception e)
            {
                return getMessage(e);
            }
            return "正常";
        }

        public string UploadMedia(string thumbnail, string title, string artist)
        {
            // put /api/media
            var request = new RestRequest("api/media", Method.Put);
            var param = new { thumbnail, title, artist };
            request.AddJsonBody(param);
            try
            {
                client.Put(request);
            }
            catch (Exception e)
            {
                return getMessage(e);
            }
            return "正常";
        }

        public string UploadMediaPlayback(bool playing)
        {
            // put /api/media_playback
            var request = new RestRequest("api/media_playback", Method.Put);
            var param = new { playing };
            request.AddJsonBody(param);
            try
            {
                client.Put(request);
            }
            catch (Exception e)
            {
                return getMessage(e);
            }
            return "正常";
        }

        public string Heartbeat()
        {
            // put /api/heartbeat
            var request = new RestRequest("api/heartbeat", Method.Get);
            try
            {
                client.Get(request);
            }
            catch (Exception e)
            {
                return getMessage(e);
            }
            return "正常";
        }

        public string UploadMediaTimeline(string current, string total)
        {
            if(TimelineUploading)
            {
                return "忙碌";
            }
            TimelineUploading = true;
            // put /api/media_timeline
            var request = new RestRequest("api/media_timeline", Method.Put);
            var param = new { current, total };
            request.AddJsonBody(param);
            try
            {
                client.Put(request);
            }
            catch (Exception e)
            {
                TimelineUploading = false;
                return getMessage(e);
            }
            TimelineUploading = false;
            return "正常";
        }
    }
}
