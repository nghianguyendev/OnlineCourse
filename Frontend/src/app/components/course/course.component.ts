import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CourseModel } from 'src/shared/models/course.model';
import { CourseService } from 'src/shared/services/course.service';
import { Observable } from 'rxjs';
import { interval } from "rxjs";
import { take, map } from "rxjs/operators";


@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.scss']
})
export class CourseComponent implements OnInit {
  course: CourseModel = null;
  courseForm: FormGroup;
  courses: CourseModel[];
  numberOfCourse: Observable<number>;

  constructor(private formBuilder: FormBuilder,
    private courseService: CourseService) { 
    this.courseForm = this.formBuilder.group({
      'name': new FormControl('', Validators.required),
      'url': new FormControl('', Validators.required),
      'category': new FormControl('')
    });

    this.courseForm.valueChanges
        .subscribe( data => console.log(JSON.stringify(data)));
  }

  ngOnInit() {
    this.courseService.getAll()
        .subscribe(courses => { this.courses = courses; });
        this.numberOfCourse = this.getObservable();
  }

  onSubmit( ) {    
    if (this.courseForm.valid) {
      console.log("Form Submitted!");
    }else{
      console.log("Form not Submitted!");
      console.log(this.courseForm.controls.name.valid);
    }
  }

  getObservable() {
    return interval(1000).pipe(
      take(10),
      map(v => v * v)
    );
  }

  getNumberOfCourse(){
    return this.courseService.getAll().pipe(map(this.mapData));    
  }

  private mapData(res: Response): any {
    let body;
    try {
      // check if empty, before call json
      if (res.text()) {
        body = res.json();
      }
    } catch (error) {
      // Its not JSON, return a text
      body = res.text();
    }
    debugger
    return body.length;
    // return body || {};
  }

}
