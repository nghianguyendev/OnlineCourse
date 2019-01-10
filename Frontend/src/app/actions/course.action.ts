import { Injectable } from '@angular/core';
import { Action } from '@ngrx/store';
import { CourseModel } from 'src/shared/models/course.model';

 
export const CREATE_COURSE = 'Course_Create';
export const ADD_COURSE = 'Course_Add';
export const DELETE_COURSE = 'Course_Delete';
 
export class CreateCourse implements Action {
    readonly type = CREATE_COURSE;
 
    constructor(public payload: CourseModel) { }
}

export class AddCourse implements Action {
    readonly type = ADD_COURSE;
 
    constructor(public payload: CourseModel) { }
}

export class DeleteCourse implements Action {
    readonly type = DELETE_COURSE;
 
    constructor(public id: number) { }
}
 
export type Actions = CreateCourse | AddCourse | DeleteCourse;