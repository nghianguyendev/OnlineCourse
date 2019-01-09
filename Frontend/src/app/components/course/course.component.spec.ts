import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CourseComponent } from './course.component';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { CourseService } from 'src/shared/services/course.service';
import { of } from 'rxjs';
import { Count } from 'src/shared/pipes/count.pipe';
class MockCourseService {
 
  getAll() {
    var courses = [{name : "course1"}];
    return of(courses);
}
}

describe('CourseComponent', () => {
  let component: CourseComponent;
  let fixture: ComponentFixture<CourseComponent>;
  const formBuilder: FormBuilder = new FormBuilder();
  const courseService = new MockCourseService();

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CourseComponent, Count],
      providers: [
        { provide: FormBuilder, useValue: formBuilder },
        { provide: CourseService, useValue: courseService }
      ],
      imports: [ReactiveFormsModule, RouterTestingModule]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CourseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('ngOnInit should get all courses', () =>{
    expect(component.courses.length).toEqual(1);
  })
});
