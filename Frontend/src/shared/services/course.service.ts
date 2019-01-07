import { Injectable, Inject } from '@angular/core';
import { SecureApi } from 'src/core/secure.api';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private courseApiUrl = '/api/course';

  constructor(private secureApi: SecureApi,
    @Inject('apiUrl') private apiUrl: string) { }

    getAll() {
      this.secureApi.api = this.apiUrl;
      return this.secureApi.get(`${this.courseApiUrl}/GetAll`);
  }

}
