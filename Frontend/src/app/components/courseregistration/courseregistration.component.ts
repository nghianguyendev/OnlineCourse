import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { AppState } from 'src/app/app.state';
import { CourseModel } from 'src/shared/models/course.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-courseregistration',
  templateUrl: './courseregistration.component.html',
  styleUrls: ['./courseregistration.component.scss']
})
export class CourseregistrationComponent implements OnInit {
  courses: Observable<CourseModel[]>;
  constructor(private store: Store<AppState>) { 
    this.courses = store.select('course');

  }

  ngOnInit() {
  }

}
