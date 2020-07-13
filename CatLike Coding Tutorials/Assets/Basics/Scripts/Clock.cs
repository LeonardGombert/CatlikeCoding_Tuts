using System;
using UnityEngine;

namespace CatlikeCoding.Basics.GameObjects
{
    public class Clock : MonoBehaviour
    {
        [SerializeField] Transform hoursTransform, minutesTransform, secondsTransform;
        [SerializeField] const float degreesPerHour = 30f, degreesPerMinute = 6f, degreesPerSecond = 6f;
        [SerializeField] bool continuous;

        void Update()
        {
            if (continuous) UpdateContinous();
            else UpdateDiscrete();
        }

        void UpdateContinous()
        {
            TimeSpan time = DateTime.Now.TimeOfDay;
            hoursTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalHours * degreesPerHour, 0f);
            minutesTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalMinutes * degreesPerMinute, 0f);
            secondsTransform.localRotation = Quaternion.Euler(0f, (float)time.TotalSeconds * degreesPerSecond, 0f);
        }

        void UpdateDiscrete()
        {
            DateTime time = DateTime.Now;
            hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
            minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
            secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
        }
    }
}