using System;
using System.Collections.Generic;
using System.Reflection;

namespace ObjectInspection
{
	public sealed class ObjectInspector
	{
		
		public ObjectInspector getInstance() {
			return this;
		}
		
		public Type getType(object ins_object) {
			if (ins_object == null) {
				return null;
			} else {
				return this.getType(ins_object);
			}
		}
		
		public PropertyInfo[] getProperties(object ins_object) {
        	if (ins_object == null) {
				return new PropertyInfo[] {};
			} else {
        		Type t = ins_object.GetType();
        		PropertyInfo[] props = t.GetProperties();
        		return props;				
			}
		}
		
		public FieldInfo[] getFields(object ins_object) {
        	if (ins_object == null) {
				return new FieldInfo[] {};
			} else {
        		Type t = ins_object.GetType();
        		FieldInfo[] fields = t.GetFields();
        		return fields;
			}
    	}		
		
		public MethodInfo[] getMethods(object ins_object) {
        	if (ins_object == null) {
				return new MethodInfo[] {};
			} else {
        		Type t = ins_object.GetType();
        		MethodInfo[] methods = t.GetMethods();
				return methods;
			}
    	}		
		
		public string[] getPropertiesName(object ins_object) {	
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		PropertyInfo[] props = t.GetProperties();
        		List<string> propNames = new List<string>();
        		foreach (PropertyInfo prp in props)
        		{
            		Console.WriteLine(prp.Name);
					propNames.Add(prp.Name);
        		}
        		return propNames.ToArray();
			}
    	}
		
		//public object getPropertyValue(object ins_object, 
		
		public string[] getFieldsName(object ins_object) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		FieldInfo[] fields = t.GetFields();
        		List<string> fieldsNames = new List<string>();
        		foreach (FieldInfo fld in fields)
        		{
            		fieldsNames.Add(fld.Name);
        		}
        		return fieldsNames.ToArray();
			}
    	}
		
		public string[] getMethodsName(object ins_object) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		MethodInfo[] methods = t.GetMethods();
        		List<string> methodNames = new List<string>();
        		foreach (MethodInfo mtd in methods)
        		{
            		methodNames.Add(mtd.Name);
        		}
        		return methodNames.ToArray();
			}
    	}
		
		public string[] getEventsName(object ins_object) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		EventInfo[] events = t.GetEvents();
        		List<string> eventsNames = new List<string>();
        		foreach (EventInfo evnt in events)
        		{
            		eventsNames.Add(evnt.Name);
        		}
        		return eventsNames.ToArray();
			}
    	}		
				
		public string[] getMethodsFilterBy(object ins_object, String pattern) {
			if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		MethodInfo[] methods = t.GetMethods();
        		List<string> methodNames = new List<string>();
        		foreach (MethodInfo mtd in methods)
        		{
					if (System.Text.RegularExpressions.Regex.IsMatch(mtd.Name, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
						methodNames.Add(mtd.Name);
					}
        		}
        		return methodNames.ToArray();
			}
		}
				
		public string[] getPropertiesFilterBy(object ins_object, String pattern) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		PropertyInfo[] props = t.GetProperties();
        		List<string> propNames = new List<string>();
        		foreach (PropertyInfo prp in props)
        		{
					if (System.Text.RegularExpressions.Regex.IsMatch(prp.Name, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
						propNames.Add(prp.Name);
					}
        		}
        		return propNames.ToArray();
			}			
		}
		
		public string[] getFieldsFilterBy(object ins_object, String pattern) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		FieldInfo[] fields = t.GetFields();
        		List<string> fieldsNames = new List<string>();
        		foreach (FieldInfo fld in fields)
        		{
            		if (System.Text.RegularExpressions.Regex.IsMatch(fld.Name, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
						fieldsNames.Add(fld.Name);
					}
        		}
        		return fieldsNames.ToArray();
			}			
		}
		
		public string[] getEventsFilterBy(object ins_object, String pattern) {
        	if (ins_object == null) {
				return new string[] {};
			} else {
        		Type t = ins_object.GetType();
        		EventInfo[] events = t.GetEvents();
        		List<string> eventsNames = new List<string>();
        		foreach (EventInfo evnt in events)
        		{
            		if (System.Text.RegularExpressions.Regex.IsMatch(evnt.Name, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase)) {
						eventsNames.Add(evnt.Name);
					}            		
        		}
        		return eventsNames.ToArray();
			}			
		}
		
		public object getPropertyValue(object ins_object, String property) {
			if (ins_object == null) {
				return null;
			} else {
				Type t = ins_object.GetType();
				return t.GetProperty(property).GetValue(ins_object, null);
			}
		}

		public /*Dictionary<string, string>*/ void getPropertyMap(object ins_object) {
			if (ins_object == null) {
				//return null;
			} else {
				Type t = ins_object.GetType();
				PropertyInfo[] props = t.GetProperties();
				//return t.GetProperty(property).GetValue(ins_object, null);
				foreach (PropertyInfo prp in props)
				{
					Console.WriteLine(prp.Name);
					Console.WriteLine(prp.GetValue(ins_object, null));

					//propNames.Add(prp.Name);
				}
			}			
		
		}

		public object getFieldValue(object ins_object, String field) {
			if (ins_object == null) {
				return null;
			} else {
				Type t = ins_object.GetType();
				return t.GetField(field).GetValue(ins_object);
			}
		}
		
	}
}

