namespace zipper
{
    public abstract class UserInterface
    {
        public abstract void init();
        public abstract List<string> MultiSelectFromList(string[] prefix, string[] items, string[] suffix,
            ConsoleColor selectedStyling = ConsoleColor.Green,
            ConsoleColor highlightStyling = ConsoleColor.DarkGray);
        public abstract int SingleSelectFromList(string[] prefix, string[] items, string[] suffix,
            ConsoleColor selectedStyling = ConsoleColor.Green,
            ConsoleColor highlightStyling = ConsoleColor.DarkGray);
    }
}