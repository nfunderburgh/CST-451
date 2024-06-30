using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace BibleApp.Models
{
    public class BibleModel
    {

        private string[] BookName = new string[66]{ "Genesis","Exodus","Leviticus","Numbers","Deuteronomy","Joshua","Judges","Ruth","1 Samuel","2 Samuel","1 Kings","2 Kings","1 Chronicles","2 Chronicles","Ezra","Nehemiah","Esther","Job","Psalm","Proverbs","Ecclesiastes","Song of Solomon","Isaiah",
         "Jeremiah","Lamentations","Ezekiel","Daniel","Hosea","Joel","Amos","Obadiah","Jonah","Micah","Nahum","Habakkuk","Zephaniah",
            "Haggai","Zechariah","Malachi","Matthew","Mark","Luke","John","Acts","Romans","1Corinthians","2Corinthians","Galatians",
            "Ephesians","Philippians","Colossians","1 Thessalonians","2 Thessalonians","1 Timothy","2 Timothy","Titus","Philemon","Hebrews", "James","1 Peter","2 Peter","1 John","2 John","3 John","Jude","Revelation"
        };

        public int VerseId { get; set; }
        public int Book { get; set; }
        [DisplayName("Book Text")]
        public string BookText { get; set; }
        public int Chapter { get; set; }
        public int Verse { get; set; }
        public string Text { get; set; }



        public BibleModel(int verseId, int book, int chapter, int verse, string text)
        {
            VerseId = verseId;
            BookText = BookName[book - 1];
            Book = book;
            Chapter = chapter;
            Verse = verse;
            Text = text;
        }

        public BibleModel()
        {

        }
    }
}
