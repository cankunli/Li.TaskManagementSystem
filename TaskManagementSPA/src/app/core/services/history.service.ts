import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { history } from 'src/app/shared/models/history';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  constructor(private apiService:ApiService ) { }

  getAllHistory() : Observable<history[]>{
    return this.apiService.getAll('TaskHistory');
  }

  createHistory(historyCreate: history): Observable<any>{
    return this.apiService.create('TaskHistory', historyCreate);
  }

  updateHistory(historyUpdate: history): Observable<any>{
    return this.apiService.update('TaskHistory', historyUpdate);
  }

  listHistoryByUser(userId : number):Observable<history[]>{
    return this.apiService.getAllById('TaskHistory/user/', userId);
  }

  revertTask(id: number): Observable<any> {
    return this.apiService.getOne('TaskHistory/revert/', id);
  }
}
