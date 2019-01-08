export class User {
    constructor(public uname: string, public upassword: string) {
        this.username = uname;
        this.password = upassword;
    }

    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
    token?: string;
}