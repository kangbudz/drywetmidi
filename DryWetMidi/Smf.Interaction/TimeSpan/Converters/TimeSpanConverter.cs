﻿using System;
using System.Collections.Generic;

namespace Melanchall.DryWetMidi.Smf.Interaction
{
    internal static class TimeSpanConverter
    {
        #region Constants

        private static readonly Dictionary<Type, ITimeSpanConverter> Converters = new Dictionary<Type, ITimeSpanConverter>
        {
            [typeof(MidiTimeSpan)] = new MidiTimeSpanConverter(),
            [typeof(MetricTimeSpan)] = new MetricTimeSpanConverter(),
            [typeof(MusicalTimeSpan)] = new MusicalTimeSpanConverter(),
            [typeof(BarBeatTimeSpan)] = new BarBeatTimeSpanConverter(),
            [typeof(MathTimeSpan)] = new MathTimeSpanConverter()
        };

        #endregion

        #region Methods

        public static TTimeSpan ConvertTo<TTimeSpan>(long timeSpan, long time, TempoMap tempoMap)
            where TTimeSpan : ITimeSpan
        {
            return (TTimeSpan)GetConverter<TTimeSpan>().ConvertTo(timeSpan, time, tempoMap);
        }

        public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan timeSpan, long time, TempoMap tempoMap)
            where TTimeSpan : ITimeSpan
        {
            return ConvertTo<TTimeSpan>(ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
        }

        public static ITimeSpan ConvertTo(ITimeSpan timeSpan, Type timeSpanType, long time, TempoMap tempoMap)
        {
            return GetConverter(timeSpanType).ConvertTo(ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
        }

        public static long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
        {
            return GetConverter(timeSpan.GetType()).ConvertFrom(timeSpan, time, tempoMap);
        }

        private static ITimeSpanConverter GetConverter<TTimeSpan>()
            where TTimeSpan : ITimeSpan
        {
            return GetConverter(typeof(TTimeSpan));
        }

        private static ITimeSpanConverter GetConverter(Type timeSpanType)
        {
            ITimeSpanConverter converter;
            if (Converters.TryGetValue(timeSpanType, out converter))
                return converter;

            throw new NotSupportedException($"Converter for {timeSpanType} is not supported.");
        }

        #endregion
    }
}