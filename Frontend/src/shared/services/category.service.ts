import { Injectable, Inject } from '@angular/core';
import { SecureApi } from 'src/core/secure.api';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiUrl = '/api/vbm';

  constructor(private secureApi: SecureApi,
    @Inject('url') private url:string) { }

}
