﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace WCFDynamicProxy
{
    public class DynamicProxy: DynamicObject
    {
        public DynamicProxy(Type proxyType, Binding binding,
                EndpointAddress address)
            : base(proxyType)
        {
            Type[] paramTypes = new Type[2];
            paramTypes[0] = typeof(Binding);
            paramTypes[1] = typeof(EndpointAddress);

            object[] paramValues = new object[2];
            paramValues[0] = binding;
            paramValues[1] = address;

            CallConstructor(paramTypes, paramValues);
        }

        public Type ProxyType
        {
            get
            {
                return ObjectType;
            }
        }

        public object Proxy
        {
            get
            {
                return ObjectInstance;
            }
        }

        public void Close()
        {
            CallMethod("Close");
        }
    }
}
