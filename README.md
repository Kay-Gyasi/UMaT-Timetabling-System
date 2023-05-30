**TODO**

- Work on course distribution
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

**Timetable creation logic**
- Randomly assign lectures to time slots without adding rooms
- Rooms will be added after time slots have been taken care of
- 