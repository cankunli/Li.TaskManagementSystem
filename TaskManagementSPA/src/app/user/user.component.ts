import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ApiService } from '../core/services/api.service';
import { UserService } from '../core/services/user.service';
import { history } from '../shared/models/history';
import { task } from '../shared/models/task';
import { User } from '../shared/models/user';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  invalidRegister: boolean = false;
  submitted = false;
  createForm!: FormGroup;
  public isCollapsedTable = true;
  public isCollapsed = true;
  users:User[] | undefined;
  tasks:task[] | undefined;
  histories:history[] | undefined;
  constructor(private userService:UserService, private apiService:ApiService, private fb: FormBuilder) { }

  get f() { return this.createForm.controls; }


  ngOnInit(): void {
    this.userService.getAllUser().subscribe(u=>{
      this.users = u;
      console.table(this.users);
    })
    this.buildForm();
  }

  buildForm() {
    this.createForm=this.fb.group({
      id: [''],
      email: [''],
      password: [''],
      fullname: [''],
      mobileno:['']
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
    this.userService.createUser(this.createForm.value).subscribe(
      data => {
        window.location.reload();
        },
        (error) => {
          this.invalidRegister = true;
        }
        )
  }
  else{
    this.userService.updateUser(this.createForm.value).subscribe(
      data => {
        window.location.reload();
        });
  }
}

deleteUserById(userId : number){
  this.apiService.delete('User/', userId).subscribe(e=>{
    console.table(e);
    window.location.reload();
  })
}

// getTaskByUserId(userId : number){
//   this.userService.listTaskByUser(userId).subscribe(e=>{
//     this.tasks = e;
//     console.table(e);
//   })
// }

// getHistoryByUserId(userId : number){
//   this.userService.listHistoryByUser(userId).subscribe(e=>{
//     this.histories = e;
//     console.table(e);
//   })
// }

}