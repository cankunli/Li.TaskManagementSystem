import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../core/services/api.service';
import { TaskService } from '../core/services/task.service';
import { UserService } from '../core/services/user.service';
import { task } from '../shared/models/task';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {

  invalidRegister: boolean = false;
  submitted = false;
  createForm!: FormGroup;
  public isCollapsedTable = true;
  public isCollapsed = true;
  users:User[] | undefined;
  tasks:task[] | undefined;
  //histories:history[] | undefined;
  constructor(private taskService:TaskService, private apiService:ApiService, private fb: FormBuilder, private route:ActivatedRoute, private userService:UserService) { }

  get f() { return this.createForm.controls; }

  ngOnInit(): void {
    
    const id = Number(this.route.snapshot.params['id']);
    //this.getUserInfo(id);

    this.taskService.listTaskByUser(id).subscribe(
      m => {
        this.tasks = m;
        console.table(this.tasks);
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
      priority:[''],
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
      this.taskService.createTask(this.createForm.value).subscribe(
        data => {
          window.location.reload();
          },
          (error) => {
            this.invalidRegister = true;
          }
          )
    }
    else{
      this.taskService.updateTask(this.createForm.value).subscribe(
        data => {
          window.location.reload();
          });
    }
  }
  
  deleteTaskById(taskId : number){
    this.apiService.delete('Task/', taskId).subscribe(e=>{
      console.table(e);
      window.location.reload();
    })
  }
  
  getTaskByUserId(userId : number){
    this.taskService.listTaskByUser(userId).subscribe(e=>{
      this.tasks = e;
      console.table(e);
    })
  }

  completeTask(taskId : number) {
    this.taskService.completeTask(taskId).subscribe(
      (res) => {
        console.log("Completed");
        window.location.reload();
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getUserInfo(userId : number){
    this.userService.getUserById(userId).subscribe(e=>{
      this.users = e;
      console.table(e);
    })
  }
  

}
