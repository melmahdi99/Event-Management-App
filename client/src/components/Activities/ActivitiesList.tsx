import type { Activity } from "../../types";

import {Grid} from '@mui/material';
import ActivityCard from "./ActivityCard";

type Props = {
    activities: Activity[];
    setSelectedActivity: (id: string) => void;
}

//prop drilling
function ActivitiesList({activities, setSelectedActivity} : Props) {

            return (
                <>
                <Grid container spacing={2}>
                    {activities.map(a => {
                        return(
                            <ActivityCard key={a.id} activity={a} setSelectedActivity={setSelectedActivity}/>
                        )
                    })}
                </Grid>
                </>
            )
}

export default ActivitiesList