# MfGames Culture CIL

* Languages: C#
* License: MIT

MfGames Culture is a CIL-based library for creating, manipulating, and querying cultures in a generic manner that differs from how the provided System.Globalization namespace handles these aspects. In specific, it is designed to work with cultures that don't exist in modern day Earth, such as historical and fictional cultures, that may not have the concept of hours, minutes, or days.

While it doesn't use the built-in functionality, it still attempts to utilize various ISO standards, such as county and language codes, to simplify integration with other systems.

Definitions for various elements are written in XML.

Calendar
===============================

The initial purpose of the library is to implement the parsing, formatting, and manipulation of fictional calendars.
