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
using System.IO;
using UnityEngine;

public class StringReaderEx : StringReader
{
	public StringReaderEx(string s) : base(s)
	{

	}

	public float ReadLineFloat()
	{
		return float.Parse(ReadLine());
	}

	public Vector3 ReadLineVector3()
	{
		string line = ReadLine();
		Debug.Log(line);
		var parts = line.Split(';');
		return new Vector3(
			float.Parse(parts[0]),
			float.Parse(parts[1]),
			float.Parse(parts[2]));
	}

	public int ReadLineInt()
	{
		string line = ReadLine();
		try
		{
			return Convert.ToInt32(line);
		}
		catch (Exception e)
		{
			Debug.LogError("Integer format error while reading: "+line+" "+e.Message);
			throw new UnityException("Load error");
		}
	}

	public bool ReadLineBool()
	{
		return bool.Parse(ReadLine());
	}

	public T ReadLineEnum<T>()
	{
		return (T)Enum.Parse(typeof(T),ReadLine());
	}
}


