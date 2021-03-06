﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alice.Common;

namespace Alice.Framework
{
    public class AliceContext
    {        
        private static AliceContext _current;
        public static AliceContext Current
        {
            get
            {
                if (_current == null)
                    _current = new AliceContext();

                return _current;
            }
        }

        private AliceContext()
        {
            Register(new DefaultAliceRequestHandler());
        }

        private List<IAliceRequestHandler> _registeredHandlers;

        public void Register(IAliceRequestHandler handler)
        {
            if (_registeredHandlers == null)
                _registeredHandlers = new List<IAliceRequestHandler>();

            _registeredHandlers.Add(handler);
        }

        public IList<IAliceRequestHandler> Handlers { get { return _registeredHandlers; } }

        private UserProfile _userProfile;
        public UserProfile UserProfile
        {
            get
            {
                if (_userProfile == null)
                    _userProfile = new UserProfile();

                return _userProfile;
            }
        }
    }
}
