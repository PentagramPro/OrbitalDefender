//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.18408
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;
using System.IO;

public class StringWriterEx : StringWriter
{
	public void WriteLine(Vector3 vec)
	{
		string line = string.Format("{0};{1};{2}",vec.x,vec.y,vec.z);
		Debug.Log(line);
		WriteLine(line);
	}

	public void WriteLineEnum<T>(T value)
	{
		WriteLine(Enum.GetName(typeof(T),value));
	}
}


