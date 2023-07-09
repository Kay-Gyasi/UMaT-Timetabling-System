export class Navigations {
  public dashboard = "/";
  public rooms = "/rooms";
  public courses = "/courses";
  public classes = "/classes";
  public lectures = "/lectures";
  public addRoom = "/rooms/add"
  public editRoom(id:number){
    return `/rooms/edit/${id}`;
  }
  public editCourse(id:number){
    return `/courses/edit/${id}`;
  }
  public editLecture(id:number){
    return `/lectures/edit/${id}`;
  }
}
