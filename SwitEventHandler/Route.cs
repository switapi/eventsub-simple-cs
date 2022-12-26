using System;
using System.IO;
using System.Net;
using System.Text;

using SwitEvent;
using SwitEvent.Approval;

namespace SwitEventHandler
{
    public static class Route
    {
        public const string SWIT_SECRET = "";
        public const string SWIT_EVENT_ROUTE = "event";

        public static void ProcessSwitEvent(HttpListenerRequest req, HttpListenerResponse rsp)
        {
            try
            {
                Console.WriteLine("ProcessSwitEvent");

                string body = "";
                using (var reader = new StreamReader(req.InputStream))
                {
                    try
                    {
                        Console.WriteLine("Read body");
                        body = reader.ReadToEnd();
                        Console.WriteLine(body);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        return;
                    }
                }

                Console.WriteLine("Check hash");
                bool valid = EventSecurity.ValidSwitSecrets(req.Headers, body, SWIT_SECRET);
                if (!valid)
                {
                    rsp.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                Console.WriteLine("Parse body");
                EventAPIParam evt = EventParams.ParseBody(body);
                if (evt == null)
                {
                    rsp.StatusCode = (int)HttpStatusCode.InternalServerError;
                    return;
                }

                Console.WriteLine("Process {0}", evt.event_type);
                if (evt.event_type == EventType.EventURLVerification)
                {
                    rsp.AddHeader("Content-Type", "text");

                    // Construct a response.
                    byte[] buffer = Encoding.UTF8.GetBytes(evt.verification_code);

                    // Get a response stream and write the response to it.
                    Stream output = rsp.OutputStream;
                    output.Write(buffer, 0, buffer.Length);

                    Console.WriteLine("Send URL Verification");
                }
                else if (evt.event_type == EventType.EventApprovalCreate ||
                        evt.event_type == EventType.EventApprovalUpdate ||
                        evt.event_type == EventType.EventApprovalDelete ||
                        evt.event_type == EventType.EventApprovalRequest ||
                        evt.event_type == EventType.EventApprovalRecall ||
                        evt.event_type == EventType.EventApprovalApprove ||
                        evt.event_type == EventType.EventApprovalReject ||
                        evt.event_type == EventType.EventApprovalLastApprove)
                {
                    ApprovalInfo ai = EventParams.ParseAPI<ApprovalInfo>(evt.data);

                    if (evt.event_type == EventType.EventApprovalLastApprove || ai.is_last_approve)
                    {
			            ai.referrer_teams = ai.last_approve_referrer_teams;
                    }
                }
                else
                {
                    Console.WriteLine("There is no event type on list:{0}", evt.event_type);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                if (rsp != null)
                {
                    rsp.StatusCode = (int)HttpStatusCode.InternalServerError;
                }
            }
            finally
            {
                try
                {
                    // You must close the output stream.
                    rsp.OutputStream.Close();
                    Console.WriteLine("rsp is OK");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
