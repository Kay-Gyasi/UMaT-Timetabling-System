export class Navigations {
  public dashboard = "/";
  public rooms = "/rooms";
  public classes = "/classes";
  public addRoom = "/rooms/add"
  public editRoom(id:number){
    return `/rooms/edit/${id}`;
  }
}
