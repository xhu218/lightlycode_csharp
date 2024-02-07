﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using static TcpServer.CustomProtocol;
using static System.Console;

namespace TcpServer
{
    public struct Session
    {
        public string SessionId { get; set; }
        public DateTime LastAccessTime { get; set; }
    }

    public class SessionManager
    {
        private readonly ConcurrentDictionary<string, Session> _sessions = new ConcurrentDictionary<string, Session>();
        private readonly ConcurrentDictionary<string, Dictionary<string, string>> _sessionData = new ConcurrentDictionary<string, Dictionary<string, string>>();

        public string CreateSession()
        {
            string sessionId = Guid.NewGuid().ToString();
            if (_sessions.TryAdd(sessionId, new Session { SessionId = sessionId, LastAccessTime = DateTime.UtcNow }))
            {
                return sessionId;
            }
            else
            {
                return string.Empty;
            }
        }
        public void CleanupAllSessions()
        {
            foreach (var session in _sessions)
            {
                if (session.Value.LastAccessTime + SessionTimeout >= DateTime.UtcNow)
                {
                    CleanupSession(session.Key);
                }
            }
        }

        public void CleanupSession(string sessionId)
        {
            Dictionary<string, string> removed;
            if (_sessionData.TryRemove(sessionId, out removed))
            {
                WriteLine($"removed {sessionId} from session data");
            }
            Session header;
            if (_sessions.TryRemove(sessionId, out header))
            {
                WriteLine($"removed {sessionId} from sessions");
            }
        }

        public void SetSessionData(string sessionId, string key, string value)
        {
            Dictionary<string, string> data;

            if (!_sessionData.TryGetValue(sessionId, out data))
            {
                data = new Dictionary<string, string>();
                data.Add(key, value);
                _sessionData.TryAdd(sessionId, data);
            }
            else
            {
                string val;
                if (data.TryGetValue(key, out val))
                {
                    data.Remove(key);
                }
                data.Add(key, value);
            }
        }

        public string GetSessionData(string sessionId, string key)
        {
            Dictionary<string, string> data;
            if (_sessionData.TryGetValue(sessionId, out data))
            {
                string value;
                if (data.TryGetValue(key, out value))
                {
                    return value;
                }
            }
            return STATUSNOTFOUND;
        }

        public string ParseSessionData(string sessionId, string requestAction)
        {
            string[] sessionData = requestAction.Split('=');
            if (sessionData.Length != 2) return STATUSUNKNOWN;
            string key = sessionData[0];
            string value = sessionData[1];
            SetSessionData(sessionId, key, value);
            return $"{key}={value}";
        }

        public bool TouchSession(string sessionId)
        {
            Session oldHeader;
            if (!_sessions.TryGetValue(sessionId, out oldHeader))
            {
                return false;
            }

            Session updatedHeader = oldHeader;
            updatedHeader.LastAccessTime = DateTime.UtcNow;
            _sessions.TryUpdate(sessionId, updatedHeader, oldHeader);
            return true;

        }

    }
}
