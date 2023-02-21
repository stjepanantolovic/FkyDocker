export class Party {

    constructor(name: string, email: string) {
        const nameDetails = name.split(' ');
        this.FirstName = nameDetails[0];
        this.LastName = nameDetails.length > 1 ? nameDetails[1] : '';
        this.Email = email;

    }
    Id?: string;
    Email?: string;
    FirstName?: string;
    LastName?: string
}