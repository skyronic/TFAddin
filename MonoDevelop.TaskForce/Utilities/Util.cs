// 
// Util.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2009 Anirudh Sanjeev
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using MonoDevelop.Core.Serialization;
using System.Xml;
using System.IO;

namespace MonoDevelop.TaskForce.Utilities
{


	public static class Util
	{
		public static DataContext context = new DataContext ();

		public static void AddTypeToContext (Type t)
		{
			context.IncludeType (t);
		}

		public static string SerializeObjectToString (object o)
		{
			LogUtil log = new LogUtil ("Util.Serialization");
			// log.DEBUG("Serializing - " + o.ToString());
			//StringBuilder resultString;
			context.IncludeType (o.GetType ());

			XmlDataSerializer ser = new XmlDataSerializer (context);
			//XmlTextWriter xtw = new XmlTextWriter(Console.Out);
			TextWriter serWriter = new StringWriter ();
			XmlTextWriter xtw = new XmlTextWriter (serWriter);


			ser.Serialize (xtw, o);
			string serializedString = serWriter.ToString ();
			//serializedString = serReader.ReadToEnd();

			//log.DEBUG("The serialized string is - " + serializedString);

			return serializedString;
		}

		public static object DeserializeString (string XMLString, Type serType)
		{
			context.IncludeType (serType);

			XmlDataSerializer ser = new XmlDataSerializer (context);

			TextReader serReader = new StringReader (XMLString);
			return ser.Deserialize (serReader, serType);
		}
	}
}
