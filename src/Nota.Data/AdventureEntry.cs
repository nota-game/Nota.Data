using System;

namespace Nota.Data
{
    public readonly struct AdventureEntry
    {
        public AdventureEntry(string title, int gainedExp, string description)
        {
            this.GainedExp = gainedExp;
            this.Title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description ?? "";
        }

        public int GainedExp { get; }
        public string Title { get; }
        public string Description { get; }
    }
}