# CommissionTracker

## Overview

Track commission for each day. Add jobs for a particular day, which automatically have a commission percentage applied, and are totalled up for the day.
Add tips which are are added at 100% (i.e. unaffected by the commission percentage).

See "at a glance" (a summary of) your total commission for each day over a given week... or longer.
View trends.

Commission percentage should be configurable, and be set for each day, but the default value should be remembered.
The "first day of the week" should be configurable (though, this only affects the summary view).

## Implementation notes

Each **day** has **jobs**, **tips**, and a **commission percentage**. These are added together to show a **total**.

Each **job** and **tip** has a monetary value, and optional note.

Each **week** has **days**. **Days** are automatically added to the current **week** (based on the currently set **week start day**), and can be moved between **weeks** as needed.
