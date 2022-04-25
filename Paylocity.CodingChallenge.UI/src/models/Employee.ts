import { Dependent } from "./Dependent";


export interface Employee {
    id: number;
    name: string;
    
    departmentId: number;
    annualSalary: number;
    joiningDate: string;
}