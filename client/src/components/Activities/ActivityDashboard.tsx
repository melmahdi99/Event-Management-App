import axios from "axios";
import { Grid } from "@mui/material";
import type { Activity } from '../../types';
import React, { useEffect, useState } from "react";
import ActivitiesList from "./ActivitiesList";
import ActivityDetails from "./ActivityDetails";
import ActivityCreateForm from "./ActivityCreateForm";

function ActivityDashboard(){
    const [activities, setActivities] = useState<Activity[]>([]);
    const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined);

    useEffect(() => {

        const controller = new AbortController();
        const signal = controller.signal;
        
        async function fetchData() : Promise<void> { 
            try {
                const response =
                    await axios.get<Activity[]>('https://localhost:7003/api/activities', {signal});

                setActivities(response.data);
            } catch(err) {
                 console.error(err);
            }
        }
        fetchData();
        return () => {controller.abort()};

    }, []);

    function handleView(id: string){
        setSelectedActivity(activities.find(a => a.id === id));
    }
    
    function handleCancel() {
        setSelectedActivity(undefined);
    }

    return(
        <Grid container spacing ={2}>
            <Grid size = {8}>
                <ActivitiesList activities={activities} setSelectedActivity = {handleView}/>
            </Grid>
            <Grid size = {4}>
                {selectedActivity && <ActivityDetails activity={selectedActivity} handleCancel = {handleCancel}/>}
                {!selectedActivity && <ActivityCreateForm/>}
            </Grid>
        </Grid>
    )
}

export default ActivityDashboard