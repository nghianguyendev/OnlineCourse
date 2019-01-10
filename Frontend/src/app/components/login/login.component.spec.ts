import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { LoginComponent } from "./login.component";
import { FormBuilder, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { RouterTestingModule } from "@angular/router/testing";
import { HttpClient } from "@angular/common/http";
import { AuthenticationService } from "src/shared/services/authentication.service";
import { DebugElement } from "@angular/core";
import { By } from "@angular/platform-browser";
import { User } from "src/shared/models/user.model";
import { of } from "rxjs";

describe("LoginComponent", () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let spy: any;
  let submitEl: DebugElement;
  let loginEl: DebugElement;
  let passwordEl: DebugElement;
  let formEl: DebugElement;
  const formBuilder: FormBuilder = new FormBuilder();
  const authenticationService = new AuthenticationService(null, null);

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [LoginComponent],
      providers: [
        { provide: FormBuilder, useValue: formBuilder },
        { provide: AuthenticationService, useValue: authenticationService }
      ],
      imports: [ReactiveFormsModule, RouterTestingModule]
    }).compileComponents();
  }));

  beforeEach(() => {
    spy = spyOn(authenticationService, "logout").and.returnValue(true);
    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    submitEl = fixture.debugElement.query(By.css("button"));
    loginEl = fixture.debugElement.query(By.css("input[type=text]"));
    passwordEl = fixture.debugElement.query(By.css("input[type=password]"));
    formEl = fixture.debugElement.query(By.css("form"));
  });

  afterEach(() => {
    if (fixture) {
                fixture.destroy();
            }
  });

  it("Setting enabled to false disables the submit button", () => {
    component.enabled = false;
    fixture.detectChanges();
    expect(submitEl.nativeElement.disabled).toBeTruthy();
  });

  it("Entering email and password emits loggedIn event", async(() => {
    component.enabled = true;
    spy = spyOn(authenticationService, "login").and.returnValue(
      of({ username: "test", password: "test" })
    );

    let user: User;
    loginEl.nativeElement.value = "test";
    passwordEl.nativeElement.value = "test";

      component.loggedIn.subscribe(value => {
        user = value;      
        expect(user.username).toBe("test");
        expect(user.password).toBe("test");
      });  

    formEl.triggerEventHandler("ngSubmit", null);
  }));
});
