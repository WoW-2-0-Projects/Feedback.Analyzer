export class TimerService {
    public setTimer(callback: () => void, delay: number): number {
        return window.setTimeout(callback, delay);
    }

    public clearTimer(timerId: number | null): null {
        if(timerId) window.clearTimeout(timerId);
        return null;
    }
}