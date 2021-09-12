﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ToDoApp.Models;

namespace ToDoApp.Services
{
  class FileIOServices
  {
    private readonly string PATH;

    public FileIOServices(string path)
    {
      PATH = path;
    }

    public BindingList<ToDoModel> LoadData()
    {
      var fileExists = File.Exists(PATH);
      if (!fileExists)
      {
        File.CreateText(PATH).Dispose();
        return new BindingList<ToDoModel>();
      }

      using(var reader = File.OpenText(PATH))
      {
        var fileText = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<BindingList<ToDoModel>>(fileText);
      }
    }

    public void SaveData(object toDoDataList)
    {
      using (StreamWriter writer = File.CreateText(PATH))
      {
        string output = JsonConvert.SerializeObject(toDoDataList);
        writer.Write(output);
      }
    }
  }
}
