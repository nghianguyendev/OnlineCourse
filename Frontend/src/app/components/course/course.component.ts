import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CourseModel } from 'src/shared/models/course.model';
import { CourseService } from 'src/shared/services/course.service';

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
  }

  ngOnInit() {
    this.courseService.getAll()
        .subscribe(courses => { this.courses = courses; console.log(courses) });
  }

  onSubmit( ) {    
    if (this.courseForm.valid) {
      console.log("Form Submitted!");
    }else{
      console.log("Form not Submitted!");
      console.log(this.courseForm.controls.name.valid);
    }
  }
}
