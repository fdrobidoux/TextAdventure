using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.Core
{
    public enum NamedGlyph : int
    {
        // 1 @ 0-15
        BLANK = 0,
        SMILEY = 1,
        SMILEY_INVERT = 2,
        HEART = 3,
        LOZANGE = 4,
        TREFFLE = 5,
        SPADE = 6,
        SMALLDOT = 7,
        SMALLDOT_INVERT = 8,
        BIGDOT = 9,
        BIGDOT_INVERT = 10,
        MALE = 11,
        FEMALE = 12,
        SINGLE_NOTE = 13,
        QUAVER = SINGLE_NOTE,
        DOUBLE_NOTE = 14,
        BEAMED_NOTES = DOUBLE_NOTE,
        SNOWFLAKE = 15,

        // 2 @ 16-31
        CARET_RIGHT = 16,
        CARET_LEFT = 17,
        VERTICAL_SCROLL = 18,
        TWO_EXCLAMATION_MARKS = 19, DOUBLE_EXCLAMATION_MARKS = 19,
        PARAGRAPH = 20,
        QUARTER_HEIGHT_BLOCK = 21,
        VERTICAL_SCROLL_END = 22,
        ARROW_UP = 23,
        ARROW_DOWN = 24,
        ARROW_RIGHT = 25,
        ARROW_LEFT = 26,

    }
}
