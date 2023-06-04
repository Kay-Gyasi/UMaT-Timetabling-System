**TODO**
- Use in-memory caching to cache lectures, classes etc.
- Decide how to handle combine classes
- Maximum capacity in a subclass table is 80
- Write job to handle outbox messages
- Add migrations and test processors
- Write endpoint for retrieving timetable as excel file
- Trim down metronic files (ie. assets)

**Work for IT Unit**
- Take teaching and practical hours as part of course input
- 

**Prerequisites for timetable generation**
- Creation of subclass groups is done at the timetable generation stage based on NumOfSubClasses field in ClassGroup table
- Initially add all subgroups offering a particular course to a single lecture (and provide functionality to split)
- Lectures should be confirmed by lecturer or chief examiner before timetable build can run (add an isConfirmed field to the lectures)
- Departmental admins should set NumOfSubClasses fields for each class group
