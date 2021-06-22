import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../core/services/api.service';
import { HistoryService } from '../core/services/history.service';
import { history } from '../shared/models/history';

@Component({
  selector: 'app-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.css']
})
export class HistoryComponent implements OnInit {

  invalidRegister: boolean = false;
  submitted = false;
  createForm!: FormGroup;
  public isCollapsedTable = true;
  public isCollapsed = true;
  //users:User[] | undefined;
  //tasks:task[] | undefined;
  histories:history[] | undefined;
  constructor(private historyService:HistoryService, private apiService:ApiService, private fb: FormBuilder, private route:ActivatedRoute) { }

  get f() { return this.createForm.controls; }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.params['id']);

    // this.taskService.getUserById(id).subscribe(e=>{
    //   this.tasks = e;
    //   console.table(e);
    // })

    this.historyService.listHistoryByUser(id).subscribe(
      m => {
        this.histories = m;
        console.table(this.histories);
      }
    )

    this.buildForm();
  }

  buildForm() {
    this.createForm=this.fb.group({
      id: [''],
      userid: [''],
      title: [''],
      description: [''],
      duedate:[''],
      completed:[''],
      remarks:['']
    })
  }

  onSubmit() {
    // console.log('submit clicked');
    console.table(this.createForm);
    this.submitted = true;
    // stop here if form is invalid
    if (this.createForm.invalid) {
      return;
    }
    if(this.createForm.controls['id'].value == 0){
      this.historyService.createHistory(this.createForm.value).subscribe(
        data => {
          window.location.reload();
          },
          (error) => {
            this.invalidRegister = true;
          }
          )
    }
    else{
      this.historyService.updateHistory(this.createForm.value).subscribe(
        data => {
          window.location.reload();
          });
    }
  }
  
  deleteTaskHisotryById(taskId : number){
    this.apiService.delete('TaskHistory/delete/', taskId).subscribe(e=>{
      console.table(e);
      window.location.reload();
    })
  }
  
  getTaskHistoryByUserId(userId : number){
    this.historyService.listHistoryByUser(userId).subscribe(e=>{
      this.histories = e;
      console.table(e);
    })
  }

  revertHistory(historyId : number) {
    this.historyService.revertTask(historyId).subscribe(e=>{
      this.histories = e;
      console.table(e);
      window.location.reload();
    })
  }

}