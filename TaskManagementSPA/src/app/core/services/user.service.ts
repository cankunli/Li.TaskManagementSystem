import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { history } from 'src/app/shared/models/history';
import { task } from 'src/app/shared/models/task';
import { User } from 'src/app/shared/models/user';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private apiService:ApiService) { }

  getAllUser() : Observable<User[]>{
    return this.apiService.getAll('User');
  }

  createUser(userCreate: User): Observable<any>{
    return this.apiService.create('User', userCreate);
  }

  updateUser(userUpdate: User): Observable<any>{
    return this.apiService.update('User', userUpdate);
  }

  getUserById(id: number): Observable<any> {
    return this.apiService.getOne('User/', id);
  }
  // listTaskByUser(userId : number):Observable<task[]>{
  //   return this.apiService.getAllById('Task/user/', userId);
  // }
}
