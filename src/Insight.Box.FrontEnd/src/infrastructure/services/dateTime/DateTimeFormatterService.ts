import { computed } from "vue";

export class DateTimeFormatterService {

    /* Calendar calculation */

    public getMonthDaysAsDate = (date: Date) => {
        const year = date.getFullYear();
        const month = date.getMonth();
        const daysInMonth = new Date(year, month + 1, 0).getDate();

        return Array.from({length: daysInMonth}, (_, i) => new Date(year, month, i + 1));
    }

    public getPrevMonthLastDays = (date: Date) => {
        const firstDayOfTheMonth = new Date(date.getFullYear(), date.getMonth()).getDay();
        return Array.from({length: firstDayOfTheMonth}, (_, i) => i);
    }

    public getNextMonthFirstDays = (date: Date) => {
        const lastDayOfTheMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDay();
        return Array.from({length: 6 - lastDayOfTheMonth}, (_, i) => i);
    }

    public addDays(date: Date, days: number): Date {
        const result = new Date(date);
        result.setDate(result.getDate() + days);
        return result;
    }

    public getPreviousMonth(date: Date): Date {
        return this.addDays(date, -30);
    }

    public getNextMonth(date: Date): Date {
        return this.addDays(date, 30);
    }

    public compareDates(dateA: Date, dateB: Date): -1 | 0 | 1 {
        const dateAWithoutTime = new Date(dateA.getFullYear(), dateA.getMonth(), dateA.getDate());
        const dateBWithoutTime = new Date(dateB.getFullYear(), dateB.getMonth(), dateB.getDate());

        if (dateAWithoutTime < dateBWithoutTime) {
            return -1;
        } else if (dateAWithoutTime > dateBWithoutTime) {
            return 1;
        } else {
            return 0;
        }
    }

    /* region Date formatters */

    public formatHumanize(date: Date): string {
        date = new Date(date);
        const now = new Date();

        const dayOfWeek = date.getDay();
        const startOfWeek = new Date(now.getFullYear(), now.getMonth(), now.getDate() - now.getDay());
        const endOfWeek = new Date(now.getFullYear(), now.getMonth(), now.getDate() + (6 - now.getDay()));
        const isThisWeek = date >= startOfWeek && date <= endOfWeek;
        const isThisYear = date.getFullYear() === now.getFullYear();

        const dayNames = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
        const monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];

        let formattedDate = '';
        if (isThisWeek) {
            formattedDate += dayNames[dayOfWeek] + ', ';
        }

        formattedDate += monthNames[date.getMonth()] + ' ' + date.getDate();

        if (!isThisYear) {
            formattedDate += ' ' + date.getFullYear();
        }

        let hours = date.getHours();
        const minutes = date.getMinutes();
        const ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12;

        const formattedTime = `${hours}:${minutes < 10 ? '0' + minutes : minutes} ${ampm}`;

        return `${formattedDate} ${formattedTime}`;
    }

    public formattedDate = (date: Date) => {
        const dayShort = date.toLocaleString("en-US", {weekday: 'short'});
        const monthShort = date.toLocaleString("en-US", {month: 'short'});
        const day = date.toLocaleString("en-US", {day: '2-digit'});
        const year = date.toLocaleString("en-US", {year: 'numeric'});

        return `${dayShort}, ${monthShort} ${day}, ${year}`;
    }

    /* endregion */
}