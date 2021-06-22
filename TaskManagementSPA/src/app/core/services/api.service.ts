import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  headers: HttpHeaders | { [header: string]: string | string[]; } | undefined;
  constructor(protected http:HttpClient) { }

  getAll(path: string): Observable<any[]> {
    return this.http.get(`${environment.apiUrl}${path}`).pipe(
      map(resp => resp as any[])
      );
  }

  getAllById(path: string, id?: number): Observable<any[]> {
    return this.http.get(`${environment.apiUrl}${path}` + id).pipe(
      map(resp => resp as any[])
    );
  }

  getOne(path: string, id?: number): Observable<any> {
    return this.http.get(`${environment.apiUrl}${path}` + id).pipe(
      map(resp => resp as any)
    );
  }

  create(path: string, resource: any, options?: any): Observable<any>{
    return this.http.post(`${environment.apiUrl}${path}`, resource).pipe(
      map(response => response)
    );
  }

  update(path: string,resource: any, options?:any):Observable<any>{
    return this.http
    .put(`${environment.apiUrl}${path}`,resource,{headers:this.headers}).pipe(
      map(response=>response)
      );
  }

  delete(path: string, id:number): Observable<any[]>{
    return this.http.delete(`${environment.apiUrl}${path}` + id).pipe(
      map(resp=>resp as any[])
      );
  }
}
