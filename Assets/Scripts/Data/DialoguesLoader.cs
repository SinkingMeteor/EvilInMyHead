﻿using Sheldier.Constants;
using Sheldier.Graphs.DialogueSystem;

namespace Sheldier.Data
{
    public class DialoguesLoader : AssetProvider<DialogueSystemGraph>
    {
        protected override string Path => AssetPathProvidersPaths.DIALOGUES_PROVIDER;
    }
}