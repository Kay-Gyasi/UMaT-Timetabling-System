**TODO**
- Use in-memory caching to cache lectures, classes etc.
- Decide how to handle combine classes
- Maximum capacity in a subclass table is 80
- Use subclasses as groups for timetable generation
- Read data for timetable generation from db and not from an api service
- For timetable create something that just works. Then provide a user interface for customizing it (creating subclasses (CE 2 - (CE 2A, CE 2B)), switching rooms)
- On UI, provide a way to merge classes (to create combined classes)
- Write job to handle outbox messages
- Add migrations and test processors
- Write endpoint for retrieving timetable as excel file
- Trim down metronic files (ie. assets)

**UI Features**
- Lecture Room CRUD
- Timetable creation management

**Work for IT Unit**
- Take teaching and practical hours as part of course input
- 

**Prerequisites for timetable generation**
- Creation of subclass groups is done at the timetable generation stage based on NumOfSubClasses field in ClassGroup table
- Initially add all subgroups offering a particular course to a single lecture (and provide functionality to split)
- Lectures should be confirmed by lecturer or chief examiner before timetable build can run (add an isConfirmed field to the lectures)
