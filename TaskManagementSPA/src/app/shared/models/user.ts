import { task } from "./task";
export interface User {
    id: number
    email: string
    fullname: string
    mobileno: string
    tasks: task[]
    tasksHistories: History[]
}