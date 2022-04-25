import { Dependent } from "./Dependent";


export interface Employee {
    id: number;
    name: string;
    dependents: Dependent[];
    departmentId: number;
    annualSalary: number;
    joiningDate: string;
}