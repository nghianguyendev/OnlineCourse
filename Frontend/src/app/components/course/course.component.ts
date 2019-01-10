import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CourseModel } from 'src/shared/models/course.model';
import { CourseService } from 'src/shared/services/course.service';
import { Observable, of } from 'rxjs';
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
  }

  onSubmit( ) {    
    if (this.courseForm.valid) {
      console.log("Form Submitted!");
      console.log(this.courseForm.controls.name.value);
    }else{
      console.log("Form not Submitted!");
    }
  }
}
