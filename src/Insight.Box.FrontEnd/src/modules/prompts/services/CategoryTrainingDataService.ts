/*
 * Provides category training data service
 */
import {CategoryTrainingData} from "@/modules/prompts/models/CategoryTrainingData";
import {LocalStorageService} from "@/infrastructure/storages/LocalStorageService";

export class CategoryTrainingDataService {

    private readonly localStorageService = new LocalStorageService();

    /*
     * Gets all training data
     */
    public getAllTrainingData(): Array<CategoryTrainingData> {
        let categoryTrainingData = this.localStorageService.get<Array<CategoryTrainingData>>('categoryTrainingData');
        if(categoryTrainingData == null) {
            categoryTrainingData = new Array<CategoryTrainingData>();
        }

        return categoryTrainingData;
    }

    /*
     * Gets training data
     */
    public getTrainingData(categoryId: string): CategoryTrainingData | null {
        const categoryTrainingData = this.getAllTrainingData();
        console.log('training data', categoryTrainingData);
        // return [];
        return categoryTrainingData.find(x => x.categoryId === categoryId);
    }

    /*
     * Adds training data
     */
    public setTrainingData(categoryId: string, trainingWorkflowId: string) {
        // console.log('set workflow', categoryId);
        // console.log('set workflow', trainingWorkflowId);
        // console.log('setting training data', JSON.stringify(new CategoryTrainingData(categoryId, trainingWorkflowId)));

        const categoryTrainingData = this.getAllTrainingData();
        console.log('test', categoryTrainingData);

        categoryTrainingData.push(new CategoryTrainingData(categoryId, trainingWorkflowId));
        this.localStorageService.set('categoryTrainingData', categoryTrainingData);
        // localStorage.setItem('categoryTrainingData', JSON.stringify(categoryTrainingData));
    }

    /*
     * Removes training data
     */
    public removeTrainingData(categoryId: string) {
        const categoryTrainingData = this.getAllTrainingData();
        const index = -1;
        // const index = categoryTrainingData.findIndex(x => x.categoryId === categoryId);
        if(index > -1) {
            categoryTrainingData.splice(index, 1);
            this.localStorageService.set('categoryTrainingData', categoryTrainingData);
            // localStorage.setItem('categoryTrainingData', JSON.stringify(categoryTrainingData));
        }
    }
}