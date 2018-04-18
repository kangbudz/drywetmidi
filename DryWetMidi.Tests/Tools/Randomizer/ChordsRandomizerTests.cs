﻿using Melanchall.DryWetMidi.Smf.Interaction;
using Melanchall.DryWetMidi.Tests.Common;
using Melanchall.DryWetMidi.Tools;

namespace Melanchall.DryWetMidi.Tests.Tools
{
    public sealed class ChordsRandomizerTests : LengthedObjectsRandomizerTests<Chord, ChordsRandomizingSettings>
    {
        #region Properties

        protected override LengthedObjectsRandomizer<Chord, ChordsRandomizingSettings> Randomizer { get; } = new ChordsRandomizer();

        protected override LengthedObjectMethods<Chord> Methods { get; } = new ChordMethods();

        #endregion
    }
}