using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;


/*
 * Want more WCF tips?
 * Visit http://webservices20.blogspot.com/
 */


namespace WebServices20.BindingExtenions
{
    public class ClearUsernameBinding : CustomBinding
    {
        private MessageVersion messageVersion = MessageVersion.None;

        public void SetMessageVersion(MessageVersion value)
        {
            this.messageVersion = value;
        }

        public override BindingElementCollection CreateBindingElements()
        {
            XmlDictionaryReaderQuotas rqMax = XmlDictionaryReaderQuotas.Max;
            TextMessageEncodingBindingElement textBE = new TextMessageEncodingBindingElement();
            textBE.MessageVersion = this.messageVersion;

            rqMax.CopyTo(textBE.ReaderQuotas);
            var res = new BindingElementCollection();
            res.Add(textBE);

            res.Add(SecurityBindingElement.CreateUserNameOverTransportBindingElement());
            res.Add(new AutoSecuredHttpTransportElement());

            return res;
        }

        public override string Scheme
        {
            get
            {
                return "http";
            }
        }
    }
}
