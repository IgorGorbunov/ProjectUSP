using System;
using System.Collections.Generic;

namespace ObjectInspection
{
	class ObjectInspectorTest
	{
			
		public static void Main (string[] args)
		{
			ObjectInspector obj_inspector = new ObjectInspector();
			
			LinkedList<string> testList = new LinkedList <string> ();	
			testList.AddFirst("test1");
			testList.AddAfter(testList.Last, "1test");
			
			Console.WriteLine (obj_inspector.getMethodsFilterBy(testList, "le").Length);
		}
	}
}
