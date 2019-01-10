import { CourseModel } from 'src/shared/models/course.model';
import { Actions, CREATE_COURSE, DELETE_COURSE } from '../actions/course.action';

 
const initialState: CourseModel = {
    id: 1,
    name: 'Course1',
    url: 'abc.com',
    categoryId: 1
};
 
export function reducer(
    state: CourseModel[] = [],
    action: Actions) {
 
    switch (action.type) {
        case CREATE_COURSE:
            return action.payload

        case CREATE_COURSE:
            return [...state, action.payload];

        case DELETE_COURSE:
            return state.filter(({ id }) => id !== action.id);
 
        default:
            return state;
    }
}