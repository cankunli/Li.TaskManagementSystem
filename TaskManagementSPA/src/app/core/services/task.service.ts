import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { task } from 'src/app/shared/models/task';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  constructor(private apiService:ApiService) { }

  // getAllTask() : Observable<task[]>{
  //   return this.apiService.getAll('Employees');
  // }

  createTask(taskCreate: task): Observable<any>{
    return this.apiService.create('Task', taskCreate);
  }

  updateTask(taskUpdate: task): Observable<any>{
    return this.apiService.update('Task', taskUpdate);
  }

  listTaskByUser(userId : number):Observable<task[]>{
    return this.apiService.getAllById('Task/user/', userId);
  }

  completeTask(id: number): Observable<any> {
    return this.apiService.getOne('Task/complete/', id);
  }
  
  

}
