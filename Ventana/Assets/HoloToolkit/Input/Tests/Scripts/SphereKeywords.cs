﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;

namespace HoloToolkit.Unity.InputModule.Tests
{
    public class SphereKeywords : MonoBehaviour, ISpeechHandler
    {
        public void ChangeColor(string color)
        {
            switch (color.ToLower())
            {
                case "red":
                    GetComponent<Renderer>().material.color = Color.red;
                    break;
                case "blue":
                    GetComponent<Renderer>().material.color = Color.blue;
                    break;
                case "green":
                    GetComponent<Renderer>().material.color = Color.green;
                    break;
            }
        }

        public void OnSpeechKeywordRecognized(SpeechKeywordRecognizedEventData eventData)
        {
            ChangeColor(eventData.RecognizedText);
        }
    }
}