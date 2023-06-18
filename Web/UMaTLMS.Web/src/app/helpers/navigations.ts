export class Navigations {
  public dashboard = "/";
  public rooms = "/rooms";
  public classes = "/classes";
  public lectures = "/lectures";
  public addRoom = "/rooms/add"
  public editRoom(id:number){
    return `/rooms/edit/${id}`;
  }
  public editLecture(id:number){
    return `/lectures/edit/${id}`;
  }
}
