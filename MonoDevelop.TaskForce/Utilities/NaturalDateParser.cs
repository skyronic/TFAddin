// 
// NaturalDateParser.cs
//  
// Author:
//       Anirudh Sanjeev <anirudh@anirudhsanjeev.org>
// 
// Copyright (c) 2009 Anirudh Sanjeev
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Text.RegularExpressions;

namespace MonoDevelop.TaskForce.Utilities
{


	public static class NaturalDateParser
	{
		/// <summary>
		/// Gets the month given the string. January is 1 and december is 12. Case insensitive
		/// </summary>
		/// <param name="input">
		/// A <see cref="System.String"/> string containing the month name, either as a full word or a three letter code.
		/// </param>
		/// <returns>
		/// A <see cref="System.Int32"/> which is the index of the month, and is -1 if invalid.
		/// </returns>
		public static int GetMonthFromString (string input)
		{
			string[] monthnames = "january february march april may june july august september october november december".Split (' ');
			string[] monthnamesshort = "jan feb mar apr may jun jul aug sep oct nov dec".Split (' ');


			int monthIndex = 0;
			int arrayIndex = 0;

			// Check for full month names
			foreach (string month in monthnames) {
				arrayIndex++;
				Match match = Regex.Match (input, month, RegexOptions.IgnoreCase);
				if (match.Success) {
					return arrayIndex;
				}
				// test for month
			}

			arrayIndex = 0;
			// Check for short month names
			foreach (string month in monthnamesshort) {
				arrayIndex++;
				Match match = Regex.Match (input, month, RegexOptions.IgnoreCase);

				if (match.Success) {
					return arrayIndex;
				}
			}

			// If all else fails, return a -1
			return -1;

		}

		/// <summary>
		/// Gets the day index from the string, sunday being 1 and saturday being 7. The matching is case insensitive
		/// </summary>
		/// <param name="input">
		/// A <see cref="System.String"/> which should contain only the dayname (it's okay if a little more is in the string, but not recommended"
		/// </param>
		/// <returns>
		/// An <see cref="System.Int32"/> which is the index of the day. Returns -1 if there was no matching day found
		/// </returns>
		public static int GetDayFromString (string input)
		{
			string[] daynames = "sunday monday tuesday wednesday thursday friday saturday".Split (' ');
			string[] daynamesshort = "sun mon tue wed thu fri sat".Split (' ');


			int monthIndex = 0;
			int arrayIndex = 0;

			// Check for full month names
			foreach (string day in daynames) {
				arrayIndex++;
				Match match = Regex.Match (input, day, RegexOptions.IgnoreCase);
				if (match.Success) {
					return arrayIndex;
				}
				// test for month
			}

			arrayIndex = 0;
			// Check for short month names
			foreach (string day in daynamesshort) {
				arrayIndex++;
				Match match = Regex.Match (input, day, RegexOptions.IgnoreCase);

				if (match.Success) {
					return arrayIndex;
				}
			}

			// If all else fails, return a -1
			return -1;

		}

		/// <summary>
		/// Attempts to parse the input string and retrieve a task and a date. Handles many different cases, some examples
		/// are shown below:
		///
		/// All the examples are referenced with the current time as 3:51 PM. March 27th (GMT+530)
		///
		/// Original Task string: "GSoC Proposals due before 3rd april"
		/// The Task is:"GSoC Proposals due" and the due 4/3/2009 3:51:09 PM
		///
		/// Original Task string: "Lab reports due on monday"
		/// The Task is:"Lab reports due" and the due 3/30/2009 3:51:09 PM
		///
		/// Original Task string: "Date at 8PM this saturday"
		/// The Task is:"Date at 8PM" and the due 3/28/2009 3:51:09 PM
		///
		/// Original Task string: "New years' on January 1st"
		/// The Task is:"New years'" and the due 1/1/2010 3:51:09 PM
		///
		/// Original Task string: "Study solid state pysics on tuesday"
		/// The Task is:"Study solid state pysics" and the due 3/31/2009 3:51:09 PM
		///
		/// Original Task string: "Solid state physics test next tuesday"
		/// The Task is:"Solid state physics test" and the due 4/7/2009 3:51:09 PM
		///
		/// Original Task string: "April fools' on 1st"
		/// The Task is:"April fools'" and the due 4/1/2009 3:51:09 PM
		///
		/// Original Task string: "Friend's birthday on Feb 5th"
		/// The Task is:"Friend's birthday" and the due 2/5/2010 3:51:09 PM
		///
		/// </summary>
		/// <param name="input">
		/// A <see cref="System.String"/> containing the raw task input. This will be parsed to retrieve the task name and the date of the item
		/// </param>
		/// <param name="taskString">
		/// A <see cref="System.String"/> which contains the guessed task string, after prining words describing the date, and the date itself
		/// </param>
		/// <param name="itemDate">
		/// A <see cref="DateTime"/> which will contain the date which was parsed from the string, and is only a guess/
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/> which is true if a match was found, and false if it wasn't.
		/// </returns>
		public static bool GuessDateFromString (string input, out string taskString, out DateTime itemDate)
		{

			// Reference Regexp: ^(?<task>.+)\s+(on |before |due |during )(?<dayno>\d{1,2})(?:st|nd|rd|th)? (?<monthname>.+)\W$

			// For lack of a better name, time_descriptors look for these words leading up to the time and ignores them
			// Take out the trash before Wednesday becomes "take out the trash" as the task
			string time_descriptors = "\\s(on|before|due|during|this)$";
			// TODO: Figure out a regular expression that plays nice with time descriptors. USe an ugly hack till then

			string expression_prefix = "^(?<task>.+)\\s+";
			taskString = input;
			itemDate = DateTime.Now;
			input = input + " ";
			// HACK: I don't know why I need to do it but it doesen't work otherwise
			// Case 1: Handle a dayname
			// For example "Take over the world on wednesday" or "Assignment due Thursday"

			// TODO: Check if the word "next" was typed. DONE
			// Like "Do task on next wednesday" and it might be a tuesday, in which case
			// an entire week must be added to the destination date.
			// Right now, let's just assume that the flag setting that it has to be a week
			// later is fals
			bool NextFlag = false;

			string dayname_regex = expression_prefix + "next (?<dayname>.+)\\W$";

			Match match = Regex.Match (input, dayname_regex, RegexOptions.IgnoreCase);
			if (match.Success) {
				NextFlag = true;
			} else {
				// Check without a "next", and in this case, find the closest matching day
				dayname_regex = expression_prefix + "(?<dayname>.+)\\W$";
				match = Regex.Match (input, dayname_regex, RegexOptions.IgnoreCase);
			}
			if (match.Success) {

				// bool NextFlag = false;

				// Okay, there was a match, but it has to qualify as a dayname
				string dayname = match.Groups["dayname"].Value;
				// a dayname was matched
				if (dayname.Length > 0) {
					int DayIndex = GetDayFromString (dayname);
					if (DayIndex != -1) {
						taskString = match.Groups["task"].Value;
						//Console.WriteLine("Found task as {0}", taskString);
						// We now know if a day has been explicitly specified
						//Console.WriteLine("Today is: {0}", DateTime.Now.DayOfWeek);
						int TodayIndex = GetDayFromString (DateTime.Now.DayOfWeek.ToString ());
						// Case 1: Handle the possibility that the dayindex is higher than TodayIndex
						// This means that the event is this week. Say today's a monday and event's on
						// a wednesday.
						// Also, if the word "next" is entered, then we force it to go a week ahead
						if (DayIndex > TodayIndex) {
							// item date
							itemDate = DateTime.Now;
							itemDate = itemDate.AddDays (DayIndex - TodayIndex);
						}

						// Case 2: If the day index is less than or equal to today's index,
						// then the item is obviously in the next week.
						if (DayIndex <= TodayIndex) {
							//TimeSpan length;
							//length.Days = 7 + DayIndex - TodayIndex;

							itemDate = DateTime.Now;
							itemDate = itemDate.AddDays (7 + DayIndex - TodayIndex);
						}

						// We're referencing the next week. So we push the itemdate 7 days forward
						if (NextFlag) {
							itemDate = itemDate.AddDays (7);
						}

						// Finished, don't look any more.
						taskString = Regex.Replace (taskString, time_descriptors, "", RegexOptions.IgnoreCase);

						return true;
					}
				}
			}

			// Case 2: Handle 4th January or 4th Jan
			match = Regex.Match (input, expression_prefix + "(?<dayno>\\d{1,2})(?:st|nd|rd|th)? (?<monthname>.+)\\W$", RegexOptions.IgnoreCase);
			// If the regexp does not succeed
			if (!match.Success) {
				// Handle January 4th or Jan 4th
				match = Regex.Match (input, expression_prefix + "(?<monthname>.+)\\W(?<dayno>\\d{1,2})(?:st|nd|rd|th)?.+$", RegexOptions.IgnoreCase);

				// Even the Jan 4th regexp does not succeed
				if (!match.Success) {
					// Look for a mere date
					match = Regex.Match (input, expression_prefix + "(?<dayno>\\d{1,2})(?:st|nd|rd|th)?.+$", RegexOptions.IgnoreCase);
				}
			}

			// The match succeeded in finding atleast a dayno
			if (match.Success) {
				taskString = match.Groups["task"].Value;
				// Look for a month
				string monthname = match.Groups["monthname"].Value;
				int MonthIndex = -1;
				if (monthname.Length > 0) {
					MonthIndex = GetMonthFromString (monthname);
				}

				// Find the day number
				string daynum_string = match.Groups["dayno"].Value;
				int daynum = Convert.ToInt32 (daynum_string);
				// Console.WriteLine("The dayno was found to be {0}, and month index is {1}", daynum, MonthIndex);

				// Case 1: 
				// Month is not specified
				if (MonthIndex == -1) {
					// Case 1.1: Day is in the future, probably the same month, set the day number, and flag found
					if (daynum > DateTime.Now.Day) {
						itemDate = itemDate.AddDays (daynum - DateTime.Now.Day);
					}

					// Case 1.2: Day is supposedly in past, this means that it's next month.
					if (daynum < DateTime.Now.Day) {
						itemDate = itemDate.AddDays (daynum - DateTime.Now.Day);
						itemDate = itemDate.AddMonths (1);
					}
					// Case 2: month is specified
				} else {
					// Case 2.1: MonthIndex is more than current year. probably in the near future
					if (MonthIndex >= DateTime.Now.Month) {
						itemDate = itemDate.AddDays (daynum - DateTime.Now.Day);
						itemDate = itemDate.AddMonths (MonthIndex - DateTime.Now.Month);
						Console.WriteLine ("{0}", itemDate.ToString ());
					}

					// Case 2.2: Say it's december 20th, and the task is jan 7th, then the indexes have to be handled
					if (MonthIndex < DateTime.Now.Month) {
						itemDate = itemDate.AddDays (daynum - DateTime.Now.Day);
						itemDate = itemDate.AddMonths (MonthIndex - DateTime.Now.Month);
						itemDate = itemDate.AddYears (1);
					}
				}
				taskString = Regex.Replace (taskString, time_descriptors, "", RegexOptions.IgnoreCase);
				return true;
			}

			// TODO: Add more analyzers

			// Return false if no good match was found
			return false;
		}
	}

}
